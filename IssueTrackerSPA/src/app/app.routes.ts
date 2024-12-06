import { Routes } from '@angular/router';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeeFormComponent } from './employees/employee-form/employee-form.component';
import { IssuesComponent } from './issues/issues.component';
import { IssueFormComponent } from './issues/issue-form/issue-form.component';

export const routes: Routes = [
    {
        path: "employees",
        component: EmployeesComponent
    },
    {
        path: "employees/create",
        component: EmployeeFormComponent
    },
    {
        path: "employees/:id",
        component: EmployeeFormComponent
    },
    {
        path: "issues",
        component: IssuesComponent
    },
    {
        path: "issues/create",
        component: IssueFormComponent
    },
    {
        path: "issues/:id",
        component: IssueFormComponent
    }
];
