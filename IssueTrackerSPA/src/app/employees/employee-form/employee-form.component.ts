import { JsonPipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EmployeesService } from '../../services/employees.service';
import { Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-form',
  imports: [ReactiveFormsModule, JsonPipe, RouterLink],
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  employeeFormSubscription!: Subscription;
  paramsSubscription!: Subscription;
  employeesService = inject(EmployeesService);
  isEdit = false;
  id = 0;

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService
  ) {}

  onSubmit() {
    if (!this.isEdit) {
      this.employeeFormSubscription = this.employeesService.createEmployee(this.form.value).subscribe({
        next: () => {
          this.toastrService.success('Employee successfully added');
          this.router.navigateByUrl('/employees');
        },
        error: (err) => {
          console.error(err);
          this.toastrService.error('Failed to add employee');
        }
      });
    } else {
      let data = this.form.value;
      data.id = this.id;
      console.log(data);
      this.employeesService.updateEmployee(this.id, data).subscribe({
        next: () => {
          this.toastrService.success('Employee successfully edited');
          this.router.navigateByUrl('/employees');
        },
        error: (err) => {
          console.error(err);
          this.toastrService.error('Failed to edit employee');
        }
      });
    }
  }

  ngOnDestroy(): void {
    this.employeeFormSubscription?.unsubscribe();
    this.paramsSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.paramsSubscription = this.activatedRouter.params.subscribe({
      next: (params) => {
        this.id = params['id'];
        if (!this.id) return;
          this.employeesService.getEmployee(this.id).subscribe({
            next: (employee) => {
              this.form.patchValue(employee);
              this.isEdit = true;
            },
            error: (err) => {
              console.error(err);
              this.toastrService.error('Failed to load employee data');
            }
          });
        
      },
      error: (err) => {
        console.error(err);
      }
    });

    this.form = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      position: ['', Validators.required],
    });
  }
}
