import { Component, OnInit, ViewChild } from "@angular/core";
import { TransactionsService } from "../services/transaction.service";
import { CommonModule } from "@angular/common";
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-transaction-history',
    standalone: true,
    imports: [CommonModule, MatTableModule, MatButtonModule, 
        MatCardModule, MatListModule, MatPaginatorModule, MatProgressSpinnerModule],
    templateUrl: './transaction-history.html'
}) export class TransactionHistory implements OnInit {
    dataSource: any | null = null;
    displayColumns: string[] = ['date', 'currency', 'changeDue', 'actions'];
    selectedTransaction: any | null = null;
    loading = false;
    Math = Math;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(private service: TransactionsService) {}

    ngOnInit() {
        this.dataSource = new MatTableDataSource<any>();
        this.fetchTransactionHistory();
    }

    fetchTransactionHistory() {
        this.loading = true;
        this.service.getTransactionHistory().subscribe({
            next: (data) => {
                this.dataSource.data = data;
                this.dataSource.paginator = this.paginator;
                this.loading = false;
            },
            error: () => {
                this.loading = false;
            }
        });
    }

    viewDetails(tx: any) {
        this.selectedTransaction = tx;
    }

    closeDetails() {
        this.selectedTransaction = null;
    }
}