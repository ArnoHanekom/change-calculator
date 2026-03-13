import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";

@Injectable({ providedIn: 'root'}) export class TransactionsService {
    constructor(private http: HttpClient) {}

    getCurrencies(): Observable<string[]> {
        return this.http.get<string[]>(`${environment.apiUrl}/transactions/currencies`);
    }

    createTransaction(transaction: any): Observable<any> {
        return this.http.post(`${environment.apiUrl}/transactions`, transaction);
    }

    getTransactionHistory(): Observable<any[]> {
        return this.http.get<any[]>(`${environment.apiUrl}/transactions`);
    }
}