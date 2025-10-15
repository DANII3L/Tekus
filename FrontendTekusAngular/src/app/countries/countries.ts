import { Component, OnInit } from '@angular/core';
import { TableList } from '../ui/components/table-list/table-list';
import { Column } from '../ui/models/column.interface';

@Component({
  selector: 'app-countries',
  imports: [TableList],
  templateUrl: './countries.html',
  styleUrl: './countries.css'  
})
export class Countries implements OnInit {

  columns: Column[] = [
    { key: 'id', label: 'Code' },
    { key: 'name', label: 'Country' }
  ];

  constructor() {}

  ngOnInit(): void {
  }
}
