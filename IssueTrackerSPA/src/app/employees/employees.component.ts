import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Employee } from '../types/employee';
import { EmployeesService } from '../services/employees.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employees',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit {
  employees$!: Observable<Employee[]>;
  employeesService = inject(EmployeesService);

  constructor(private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  deleteEmployee(id: number): void {
    this.employeesService.deleteEmployee(id).subscribe({
      next: () => {
        this.toastrService.success('Employee successfully deleted');
        this.loadEmployees(); // Обновляем список после удаления
      },
      error: (err: any) => {
        console.error(err);
        this.toastrService.error('Failed to delete employee');
      }
    });
  }

  private loadEmployees(): void {
    this.employees$ = this.employeesService.getEmployees();
  }
}
