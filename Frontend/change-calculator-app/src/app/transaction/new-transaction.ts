import { Component, OnInit } from "@angular/core";
import { TransactionsService } from "../services/transaction.service";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-new-transaction',
    standalone: true,
    imports: [
        CommonModule, FormsModule, MatFormFieldModule, MatInputModule,
        MatSelectModule, MatButtonModule, MatCardModule, MatListModule,
        MatProgressSpinnerModule
    ],
    templateUrl: './new-transaction.html'
}) export class NewTransaction implements OnInit {
    currencies: string[] = [];
    transaction = { currencyCode: '', amountOwed: 0, amountPaid: 0 };
    result: any;
    changeBreakdown: any;
    breakdownKeys: string[] = [];
    loading = false;

    constructor(private service: TransactionsService) {}

    ngOnInit() {
        this.service.getCurrencies().subscribe(data => {
            this.currencies = data;
            if (this.currencies.length > 0) {
                this.transaction.currencyCode = this.currencies[0];
            }
        });
    }

    submit() {
        this.loading = true;
        this.result = null;
        this.service.createTransaction(this.transaction).subscribe({
            next: (res) => {                
                this.result = res;
                this.changeBreakdown = JSON.parse(res.changeBreakdown);
                this.breakdownKeys = Object.keys(this.changeBreakdown);
                this.loading = false;
            },
            error: ()=> {
                this.loading = false;
            }
        });
    }
}