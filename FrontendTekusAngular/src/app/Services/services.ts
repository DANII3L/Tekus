import { Component, signal, ViewChild } from '@angular/core';
import { TableList } from '../ui/components/table-list/table-list';
import { Column } from '../ui/models/column.interface';
import { ServiceModal } from './service-modal/service-modal';

@Component({
  selector: 'app-services',
  imports: [TableList, ServiceModal],
  templateUrl: './services.html',
  styleUrl: './services.css'
})
export class Services {
  @ViewChild(TableList) tableList!: TableList;

  columns: Column[] = [
    { key: 'id', label: 'Code' },
    { key: 'name', label: 'Service' },
    { key: 'hourlyRate', label: 'Hourly Rate' }
  ];

  selectedService = signal<any>(null);
  showModal = signal(false);

  onAddService(): void {
    this.selectedService.set(null);
    this.showModal.set(true);
  }

  onRowSelect(service: any): void {
    this.selectedService.set(service);
    this.showModal.set(true);
  }

  onModalClose(): void {
    this.showModal.set(false);
    this.selectedService.set(null);
  }

  onSaveService(data: any): void {
    this.showModal.set(false);
    this.selectedService.set(null);
    // Recargar la tabla
    if (this.tableList) {
      this.tableList.reload();
    }
  }
}
