import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { Transaction } from '../../models/transaction.model';
import { CreateTransaction } from '../../models/create-transaction.model';
import { UpdateTransaction } from '../../models/update-transaction.model';

export interface TransactionDialogData {
  transaction?: Transaction;
}

export type TransactionDialogResult =
  | { mode: 'create'; payload: CreateTransaction }
  | { mode: 'edit'; id: string; payload: UpdateTransaction };

@Component({
  selector: 'app-transaction-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './transaction-dialog.component.html',
  styleUrl: './transaction-dialog.component.scss'
})
export class TransactionDialogComponent {
  readonly isEdit: boolean;
  readonly form: ReturnType<FormBuilder['group']>;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TransactionDialogData
  ) {
    this.isEdit = !!this.data.transaction;
    this.form = this.formBuilder.group({
      amount: [{ value: this.data.transaction?.amount ?? 0, disabled: this.isEdit }, [Validators.required, Validators.min(0.01)]],
      transactionDate: [this.data.transaction ? new Date(this.data.transaction.transactionDate) : null, Validators.required],
      transactionType: [this.data.transaction?.transactionType ?? '', [Validators.required, Validators.maxLength(100)]]
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { amount, transactionDate, transactionType } = this.form.getRawValue();
    const dateValue = transactionDate instanceof Date ? transactionDate.toISOString() : '';

    if (this.isEdit && this.data.transaction) {
      const payload: UpdateTransaction = {
        transactionDate: dateValue,
        transactionType: transactionType ?? ''
      };

      this.dialogRef.close({ mode: 'edit', id: this.data.transaction.id, payload } satisfies TransactionDialogResult);
      return;
    }

    const payload: CreateTransaction = {
      amount: amount ?? 0,
      transactionDate: dateValue,
      transactionType: transactionType ?? ''
    };

    this.dialogRef.close({ mode: 'create', payload } satisfies TransactionDialogResult);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
