import { Component, Input, Output, EventEmitter, WritableSignal, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../ui/service/api.service';

@Component({
  selector: 'app-provider-modal',
  imports: [FormsModule],
  templateUrl: './provider-modal.html',
  styleUrl: './provider-modal.css'
})
export class ProviderModal {
  @Input() provider: any = null;
  @Input() showModal: WritableSignal<boolean> = signal(false);
  @Output() onClose = new EventEmitter<void>();
  @Output() onSave = new EventEmitter<any>();

  formData = {
    nit: '',
    name: '',
    email: ''
  };

  errorMessage = '';
  isSubmitting = false;

  constructor(private apiService: ApiService) {}

  ngOnChanges(): void {
    if (this.provider) {
      this.formData = {
        nit: this.provider.nit || '',
        name: this.provider.name || '',
        email: this.provider.email || ''
      };
    } else {
      this.resetForm();
    }
  }

  resetForm(): void {
    this.formData = {
      nit: '',
      name: '',
      email: ''
    };
    this.errorMessage = '';
  }

  closeModal(): void {
    this.resetForm();
    this.onClose.emit();
  }

  saveProvider(): void {
    if (!this.formData.nit || !this.formData.name || !this.formData.email) {
      this.errorMessage = 'Por favor completa todos los campos';
      return;
    }

    // Validación básica de email
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(this.formData.email)) {
      this.errorMessage = 'Por favor ingresa un email válido';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const providerData = {
      nit: this.formData.nit,
      name: this.formData.name,
      email: this.formData.email
    };

    if (this.provider) {
      // Actualizar proveedor existente
      this.apiService.put('provider', { ...providerData, id: this.provider.id }).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          this.onSave.emit(response);
          this.closeModal();
        },
        error: (error) => {
          this.isSubmitting = false;
          this.errorMessage = error.error?.message || 'Error al actualizar el proveedor';
        }
      });
    } else {
      // Crear nuevo proveedor
      this.apiService.post('provider', providerData).subscribe({
        next: (response) => {
          this.isSubmitting = false;
          this.onSave.emit(response);
          this.closeModal();
        },
        error: (error) => {
          this.isSubmitting = false;
          this.errorMessage = error.error?.message || 'Error al crear el proveedor';
        }
      });
    }
  }
}

