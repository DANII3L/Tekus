import { Component, Input, Output, EventEmitter, signal, OnDestroy, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import Swal from 'sweetalert2';
import { ApiService } from '../../ui/service/api.service';
import { TableList } from '../../ui/components/table-list/table-list';
import { Column } from '../../ui/models/column.interface';

@Component({
  selector: 'app-provider-services-modal',
  imports: [CommonModule, FormsModule, TableList],
  templateUrl: './provider-services-modal.html',
  styleUrl: './provider-services-modal.css'
})
export class ProviderServicesModal implements OnDestroy {
  @Input() provider: any = null;
  @Input() showModal = signal(false);
  @Output() onClose = new EventEmitter<void>();
  @Output() onSave = new EventEmitter<any>();

  services = signal<any[]>([]);
  countries = signal<any[]>([]);
  selectedServices = signal<any[]>([]);
  selectedService = signal<any>(null);
  editingService = signal<any>(null);
  loading = signal(false);

  serviceColumns: Column[] = [
    { key: 'name', label: 'Service' },
    { key: 'hourlyRate', label: 'Rate' }
  ];

  countryColumns: Column[] = [
    { key: 'name', label: 'Country' },
    { key: 'id', label: 'Code' }
  ];

  private Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 2000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer);
      toast.addEventListener('mouseleave', Swal.resumeTimer);
    }
  });

  constructor(private apiService: ApiService) { 
    effect(() => {
      if (this.showModal() && this.provider) {
        this.loadData();
      }
    });
  }

  ngOnDestroy(): void {
    this.resetModalState();
  }

  loadData(): void {
    this.loading.set(true);

    forkJoin({
      services: this.apiService.get('service'),
      countries: this.apiService.get('country')
    }).subscribe({
      next: (responses) => {
        this.services.set(responses.services.data.listFind || []);
        this.countries.set(responses.countries.data.listFind || []);
        this.loading.set(false);
        this.loadProviderServices();
      },
      error: (error) => {
        console.error('Error loading data:', error);
        this.resetDataOnError();
      }
    });
  }

  loadProviderServices(): void {
    if (!this.provider) {
      this.selectedServices.set([]);
      return;
    }

    this.apiService.get(`providerservice/provider/${this.provider.id}`).subscribe({
      next: (response) => {
        const services = this.mapProviderServices(response.data || []);
        this.selectedServices.set(services);
      },
      error: (error) => {
        console.error('Error loading provider services:', error);
        this.selectedServices.set([]);
      }
    });
  }

  private mapProviderServices(data: any[]): any[] {
    return data.map((service: any) => ({
      ...service,
      countries: service.providerSeviceCountries?.map((psc: any) => ({
        id: psc.countryId,
        name: { common: psc.countryId }
      })) || []
    }));
  }

  private resetDataOnError(): void {
    this.loading.set(false);
    this.services.set([]);
    this.countries.set([]);
    this.selectedServices.set([]);
  }

  private resetModalState(): void {
    this.editingService.set(null);
    this.selectedService.set(null);
    this.selectedServices.set([]);
  }

  selectService(service: any): void {
    if (this.isServiceAlreadyAdded(service.id)) {
      this.showToast('warning', 'Service already added', 'This service is already in the selected services list');
      return;
    }
    this.selectedService.set({ ...service });
  }

  addServiceToSelected(): void {
    const service = this.selectedService();
    if (!service || !this.provider) return;

    if (this.isServiceAlreadyAdded(service.id)) {
      this.showToast('warning', 'Service Already Exists', `${service.name} is already assigned to this provider`);
      return;
    }

    const providerServiceData = this.buildProviderServiceData(service);
    this.loading.set(true);

    this.apiService.post('providerservice', providerServiceData).subscribe({
      next: () => {
        this.loading.set(false);
        this.selectedService.set(null);
        this.loadProviderServices();
        this.showToast('success', 'Service Added', `${service.name} has been added to the provider`);
      },
      error: (error) => {
        this.loading.set(false);
        this.showToast('error', 'Error', error.error?.message || 'Error adding service to provider');
      }
    });
  }

  onCountryRowSelect(country: any): void {
    this.addCountryToSelectedService(country);
  }

  removeService(service: any): void {
    this.apiService.delete(`providerservice/${service.id}`).subscribe({
      next: () => {
        this.loadProviderServices();
        this.showToast('success', 'Service Removed', `${service.serviceName} has been removed from the provider`);
      },
      error: (error) => {
        this.showToast('error', 'Error', error.error?.message || 'Error removing service');
      }
    });
  }

  onEditClick(service: any): void {
    if (this.editingService()?.id === service.id) {
      this.cancelEdit();
    } else {
      this.startEditingService(service);
    }
  }

  cancelEdit(): void {
    this.editingService.set(null);
    this.selectedService.set(null);
  }

  private startEditingService(service: any): void {
    const serviceWithCountries = {
      ...service,
      countries: service.countries || []
    };
    this.editingService.set(serviceWithCountries);
    this.selectedService.set(serviceWithCountries);
  }

  private isServiceAlreadyAdded(serviceId: number): boolean {
    return this.selectedServices().some(s => s.serviceId === serviceId);
  }

  private buildProviderServiceData(service: any): any {
    return {
      providerId: this.provider.id,
      serviceId: service.id,
      providerSeviceCountries: (service.countries || []).map((country: any) => ({
        providerServiceId: 0,
        countryId: country.id
      }))
    };
  }

  saveService(): void {
    const service = this.editingService();
    const selectedService = this.selectedService();
    if (!service?.id || !this.provider) return;

    const providerServiceData = this.buildUpdateProviderServiceData(service, selectedService);
    this.loading.set(true);

    this.apiService.put('providerservice', providerServiceData).subscribe({
      next: () => {
        this.loading.set(false);
        this.cancelEdit();
        this.loadProviderServices();
        this.showToast('success', 'Service Updated', `${service.serviceName} has been updated successfully`);
      },
      error: (error) => {
        this.loading.set(false);
        this.showToast('error', 'Error', error.error?.message || 'Error updating service');
      }
    });
  }

  addCountryToSelectedService(country: any): void {
    const service = this.selectedService();
    if (!service) return;

    const countries = service.countries || [];
    const exists = countries.find((c: any) => c.id === country.id);

    if (exists) {
      this.showToast('warning', 'Country Already Exists', `${country.name?.common || country.name} is already assigned`);
      return;
    }

    const updatedService = { ...service, countries: [...countries, country] };
    this.selectedService.set(updatedService);
    this.updateServiceInList(service.id, updatedService);
    this.showToast('success', 'Country Added', `${country.name?.common || country.name} has been added`);
  }

  removeCountryFromService(service: any, country: any): void {
    const updatedService = {
      ...service,
      countries: (service.countries || []).filter((c: any) => c.id !== country.id)
    };
    
    this.selectedService.set(updatedService);
    this.updateServiceInList(service.id, updatedService);
  }

  removeCountryFromSelectedService(country: any): void {
    const service = this.selectedService();
    if (!service) return;

    const updatedService = {
      ...service,
      countries: (service.countries || []).filter((c: any) => c.id !== country.id)
    };
    
    this.selectedService.set(updatedService);
    this.showToast('info', 'Country Removed', `${country.name?.common || country.name} has been removed`);
  }

  private updateServiceInList(serviceId: number, updatedService: any): void {
    const current = this.selectedServices();
    const updated = current.map(s => s.id === serviceId ? updatedService : s);
    this.selectedServices.set(updated);
  }

  private buildUpdateProviderServiceData(service: any, selectedService: any): any {
    const countriesToSave = selectedService?.countries || service.countries || [];
    return {
      id: service.id,
      providerId: this.provider.id,
      serviceId: service.serviceId,
      providerSeviceCountries: countriesToSave.map((country: any) => ({
        providerServiceId: service.id,
        countryId: country.id
      }))
    };
  }

  saveAll(): void {
    this.onSave.emit({
      provider: this.provider,
      services: this.selectedServices()
    });
    this.closeModal();
  }

  closeModal(): void {
    this.showModal.set(false);
    this.resetModalState();
    this.onClose.emit();
  }

  private showToast(icon: 'success' | 'error' | 'warning' | 'info', title: string, text: string): void {
    this.Toast.fire({ icon, title, text });
  }
}
