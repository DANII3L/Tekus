import { Component, signal, ViewChild } from '@angular/core';
import { Column } from '../ui/models/column.interface';
import { TableList } from '../ui/components/table-list/table-list';
import { ProviderServicesModal } from './provider-services-modal/provider-services-modal';
import { ProviderModal } from './provider-modal/provider-modal';

@Component({
  selector: 'app-providers',
  imports: [TableList, ProviderServicesModal, ProviderModal],
  templateUrl: './providers.html',
  styleUrl: './providers.css'
})
export class Providers {
  @ViewChild(TableList) tableList!: TableList;

  columns: Column[] = [
    { key: 'id', label: 'Code' },
    { key: 'nit', label: 'NIT' },
    { key: 'name', label: 'Provider' },
    { key: 'email', label: 'Email' },
  ];

  selectedProvider = signal<any>(null);
  showServicesModal = signal(false);
  showProviderModal = signal(false);

  constructor() {}

  onProviderSelect(provider: any): void {
    this.selectedProvider.set(provider);
    this.showServicesModal.set(true);
  }

  onServicesModalClose(): void {
    this.showServicesModal.set(false);
  }

  onSaveServices(data: any): void {
    this.showServicesModal.set(false);
  }

  onAddProvider(): void {
    this.selectedProvider.set(null);
    this.showProviderModal.set(true);
  }

  onProviderModalClose(): void {
    this.showProviderModal.set(false);
    this.selectedProvider.set(null);
  }

  onSaveProvider(data: any): void {
    this.showProviderModal.set(false);
    this.selectedProvider.set(null);
    if (this.tableList) {
      this.tableList.reload();
    }
  }
}
