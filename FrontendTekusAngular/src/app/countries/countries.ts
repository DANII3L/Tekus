import { Component, OnInit, signal } from '@angular/core';
import { ApiService } from '../ui/service/api';
import { Country } from './types/country.interface';
import { TableList } from '../ui/components/table-list/table-list';
import { Column } from '../ui/models/column.interface';

@Component({
  selector: 'app-countries',
  imports: [TableList],
  templateUrl: './countries.html',
  styleUrl: './countries.css'  
})
export class Countries implements OnInit {
  countries = signal<Country[]>([]);
  loading = signal(true);
  page = signal(1);
  totalRecords = signal(0);
  
  columns: Column[] = [
    { key: 'id', label: 'Code' },
    { key: 'name', label: 'Country' }
  ];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries(): void {
    const endpoint = `country/countries?page=${this.page()}`;

    this.apiService.get<any>(endpoint).subscribe({
      next: (response) => {
        this.countries.set(response.data.listFind);
        this.totalRecords.set(response.data.totalRecords);
        this.page.set(response.data.pageNumber);
        this.loading.set(false);
      },
      error: (error) => {
        console.error('Error:', error);
        this.loading.set(false);
      }
    });
  }

  onPageChange(newPage: number): void {
    this.page.set(newPage);
    this.loading.set(true);
    this.loadCountries();
  }
}
