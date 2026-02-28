import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { TransactionStatus } from '../models/transaction-status.model';

@Injectable({ providedIn: 'root' })
export class TransactionStatusService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/transactionstatuses`;

  getStatuses(): Observable<TransactionStatus[]> {
    return this.http.get<TransactionStatus[]>(this.baseUrl);
  }
}
