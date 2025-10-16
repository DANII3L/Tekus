import { Component, Input, Output, EventEmitter, WritableSignal, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../ui/service/api.service';

@Component({
  selector: 'app-service-modal',
  imports: [FormsModule],
  templateUrl: './service-modal.html',
  styleUrl: './service-modal.css'
})
export class ServiceModal {
  @Input() service: any = null;
  @Input() showModal: WritableSignal<boolean> = signal(false);
  @Output() onClose = new EventEmitter<void>();
  @Output() onSave = new EventEmitter<any>();

  formData = {
    name: '',
    hourlyRate: 0
  };

  errorMessage = '';
  isSubmitting = false;

  constructor(private apiService: ApiService) {}

  ngOnChanges(): void {
    if (this.service) {
      this.formData = {
        name: this.service.name || '',
        hourlyRate: this.service.hourlyRate || 0
      };
    } else {
      this.resetForm();
    }
  }

  resetForm(): void {
    this.formData = {
      name: '',
      hourlyRate: 0
    };
    this.errorMessage = '';
  }

  closeModal(): void {
    this.resetForm();
    this.onClose.emit();
  }

  saveService(): void {
    if (!this.formData.name || this.formData.hourlyRate <= 0) {
      this.errorMessage = 'Por favor completa todos los campos correctamente';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const serviceData = {
      name: this.formData.name,
      hourlyRate: this.formData.hourlyRate
    };

    if (this.service) {
      this.apiService.put('service', { ...serviceData, id: this.service.id }).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          this.onSave.emit(response.data);
          this.closeModal();
        },
        error: (error) => {
          this.isSubmitting = false;
          this.errorMessage = error.error?.message || 'Error al actualizar el servicio';
        }
      });
    } else {
      this.apiService.post('service', serviceData).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          this.onSave.emit(response.data);
          this.closeModal();
        },
        error: (error) => {
          this.isSubmitting = false;
          this.errorMessage = error.error?.message || 'Error al crear el servicio';
        }
      });
    }
  }
}

