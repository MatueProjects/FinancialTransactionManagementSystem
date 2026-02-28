import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { forkJoin } from 'rxjs';
import { Transaction } from '../../models/transaction.model';
import { TransactionStatus } from '../../models/transaction-status.model';
import { TransactionService } from '../../services/transaction.service';
import { TransactionStatusService } from '../../services/transaction-status.service';
import { TransactionDialogComponent, TransactionDialogResult } from './transaction-dialog.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog.component';

@Component({
  selector: 'app-transactions-page',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule
  ],
  templateUrl: './transactions-page.component.html',
  styleUrl: './transactions-page.component.scss'
})
export class TransactionsPageComponent implements OnInit {
  displayedColumns: string[] = ['id', 'transactionDate', 'transactionType', 'amount', 'statusName', 'actions'];
  dataSource = new MatTableDataSource<Transaction>([]);
  statuses: TransactionStatus[] = [];
  isLoading = false;

  constructor(
    private readonly transactionService: TransactionService,
    private readonly transactionStatusService: TransactionStatusService,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;

    forkJoin({
      transactions: this.transactionService.getTransactions(),
      statuses: this.transactionStatusService.getStatuses()
    }).subscribe({
      next: ({ transactions, statuses }) => {
        this.dataSource.data = transactions;
        this.statuses = statuses;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.handleDialogResult(result);
    });
  }

  openEditDialog(transaction: Transaction): void {
    const dialogRef = this.dialog.open(TransactionDialogComponent, {
      data: { transaction }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.handleDialogResult(result);
    });
  }

  confirmDelete(transaction: Transaction): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete transaction',
        message: `Delete ${transaction.transactionType} from ${new Date(transaction.transactionDate).toLocaleDateString()}?`
      }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (!confirmed) {
        return;
      }

      this.transactionService.delete(transaction.id).subscribe(() => {
        this.dataSource.data = this.dataSource.data.filter(item => item.id !== transaction.id);
      });
    });
  }

  private handleDialogResult(result: TransactionDialogResult): void {
    if (result.mode === 'create') {
      this.transactionService.create(result.payload).subscribe(created => {
        this.dataSource.data = [created, ...this.dataSource.data];
      });
      return;
    }

    this.transactionService.update(result.id, result.payload).subscribe(updated => {
      this.dataSource.data = this.dataSource.data.map(item => (item.id === updated.id ? updated : item));
    });
  }
}
