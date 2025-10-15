import { Component, Input, Output, EventEmitter, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Column } from '../../models/column.interface';
import { ApiService } from '../../service/api.service';

@Component({
  selector: 'app-table-list',
  imports: [FormsModule],
  templateUrl: './table-list.html',
  styleUrl: './table-list.css'
})
export class TableList implements OnInit{
  @Input() titulo: string = '';
  @Input() campos: Column[] = [];
  @Input() inputSearch: string = '';
  @Output() onEdit = new EventEmitter<any>();
  @Output() onDelete = new EventEmitter<any>();

  datos = signal<any[]>([]);
  loading = signal(true);
  page = signal(1);
  totalRecords = signal(0);
  searchTerm = '';
  private searchTimeout?: number;
  
  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadData();
  }

  onSearchChange(value: string): void {
    if (this.searchTimeout) {
      clearTimeout(this.searchTimeout);
    }

    this.searchTimeout = window.setTimeout(() => {
      this.page.set(1);
      this.loading.set(true);
      this.loadData();
    }, 500);
  }

  get totalPages(): number {
    return Math.ceil(this.totalRecords() / 10);
  }

  handleEdit(item: any): void {
    this.onEdit.emit(item);
  }

  handleDelete(item: any): void {
    this.onDelete.emit(item);
  }

  getValue(dato: any, key: string): any {
    return dato[key] !== undefined && dato[key] !== null ? dato[key] : '-';
  }

  changePage(newPage: number): void {
    if (newPage >= 1 && newPage <= this.totalPages) {
      this.onPageChange(newPage);
    }
  }

  getPageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.page() - Math.floor(maxPagesToShow / 2));
    let endPage = Math.min(this.totalPages, startPage + maxPagesToShow - 1);

    if (endPage - startPage < maxPagesToShow - 1) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    return pages;
  }

  getEndRecord(): number {
    return Math.min(this.page() * 10, this.totalRecords());
  }

  getStartRecord(): number {
    return (this.page() - 1) * 10 + 1;
  }

  loadData(): void {
    const searchParam = this.searchTerm ? `&search=${encodeURIComponent(this.searchTerm)}` : '';
    const endpoint = `${this.inputSearch}?page=${this.page()}${searchParam}`;

    this.apiService.get<any>(endpoint).subscribe({
      next: (response) => {
        this.datos.set(response.data.listFind);
        this.totalRecords.set(response.data.totalRecords);
        this.page.set(response.data.pageNumber);
        this.loading.set(false);
      },
      error: (error) => {
        this.totalRecords.set(0);
        this.page.set(1);
        this.datos.set([]);
        this.loading.set(false);
      }
    });
  }

  onPageChange(newPage: number): void {
    this.page.set(newPage);
    this.loading.set(true);
    this.loadData();
  }
}
