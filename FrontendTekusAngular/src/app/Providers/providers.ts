import { Component, signal } from '@angular/core';
import { Column } from '../ui/models/column.interface';
import { TableList } from '../ui/components/table-list/table-list';

@Component({
  selector: 'app-providers',
  imports: [TableList],
  templateUrl: './providers.html',
  styleUrl: './providers.css'
})
export class Providers {

  columns: Column[] = [
    { key: 'id', label: 'Code' },
    { key: 'nit', label: 'NIT' },
    { key: 'name', label: 'Provider' },
    { key: 'email', label: 'Email' },
  ];

  constructor() {}

  ngOnInit(): void {
  }
  
}
