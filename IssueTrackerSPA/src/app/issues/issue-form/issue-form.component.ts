import { AsyncPipe, JsonPipe } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { Observable, Subscription } from 'rxjs';
import { Issue } from '../../types/issue';
import { Employee } from '../../types/employee';
import { IssuesService } from "../../services/issue.service";
import { EmployeesService } from '../../services/employees.service';

@Component({
  selector: 'app-issue-form',
  imports: [ReactiveFormsModule, AsyncPipe, JsonPipe, RouterLink],
  templateUrl: './issue-form.component.html',
  styleUrl: './issue-form.component.css'
})
export class IssueFormComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  employees$!: Observable<Employee[]>;
  issueFormSubscription!: Subscription;
  paramsSubscription!: Subscription;
  issuesService = inject(IssuesService);
  employeesService = inject(EmployeesService);
  isEdit = false;
  id = 0;

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private toasterService: ToastrService
  ) {}

  onSubmit() {
    if (!this.isEdit) {
      console.log(this.form.value)
      this.issueFormSubscription = this.issuesService.createIssue(this.form.value).subscribe({
        next: () => {
          this.toasterService.success('Issue successfully added');
          this.router.navigateByUrl('/issues');
        },
        error: (err) => {
          console.error(err);
        },
      });
    } else {
      const data = { ...this.form.value, id: this.id };
      this.issuesService.updateIssue(this.id, data).subscribe({
        next: () => {
          this.toasterService.success('Issue successfully edited');
          this.router.navigateByUrl('/issues');
        },
        error: (err) => {
          console.error(err);
        },
      });
    }
  }

  ngOnInit(): void {
    this.employees$ = this.employeesService.getEmployees();
    this.paramsSubscription = this.activatedRouter.params.subscribe({
      next: (params) => {
        const id = params['id'];
        this.id = id;
        if (!id) return;

        this.issuesService.getIssue(id).subscribe({
          next: (issue) => {
            this.form.patchValue(issue);
            this.isEdit = true;
          },
          error: (err) => {
            console.error(err);
          },
        });
      },
      error: (err) => {
        console.error(err);
      },
    });

    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      priority: [1, [Validators.required, Validators.min(1), Validators.max(5)]],
      status: ['', Validators.required],
      dueDate: ['', Validators.required],
      employeeId: ['', Validators.required],
    });
  }

  ngOnDestroy(): void {
    this.issueFormSubscription?.unsubscribe();
    this.paramsSubscription?.unsubscribe();
  }
}
