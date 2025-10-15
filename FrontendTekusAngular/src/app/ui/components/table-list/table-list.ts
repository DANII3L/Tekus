import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Column } from '../../models/column.interface';

@Component({
  selector: 'app-table-list',
  imports: [],
  templateUrl: './table-list.html',
  styleUrl: './table-list.css'
})
export class TableList {
  @Input() titulo: string = '';
  @Input() campos: Column[] = [];
  @Input() datos: any[] = [];
  @Input() page: number = 1;
  @Input() totalRecords: number = 0;
  @Output() onEdit = new EventEmitter<any>();
  @Output() onDelete = new EventEmitter<any>();
  @Output() onPageChange = new EventEmitter<number>();

  get totalPages(): number {
    return Math.ceil(this.totalRecords / 10);
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
      this.onPageChange.emit(newPage);
    }
  }

  getPageNumbers(): number[] {
    const pages: number[] = [];
    const maxPagesToShow = 5;
    let startPage = Math.max(1, this.page - Math.floor(maxPagesToShow / 2));
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
    return Math.min(this.page * 10, this.totalRecords);
  }

  getStartRecord(): number {
    return (this.page - 1) * 10 + 1;
  }
}
