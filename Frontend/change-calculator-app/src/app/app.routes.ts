import { Routes } from '@angular/router';
import { NewTransaction } from './transaction/new-transaction';
import { TransactionHistory } from './transaction/transaction-history';

export const routes: Routes = [
    { path: '', component: NewTransaction },
    { path: 'history', component: TransactionHistory }
];
