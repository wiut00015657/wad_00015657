import { Employee } from "./employee";
import {DateTime} from "luxon";

export interface Issue {
    id: number;
    title: string;
    description: string;
    priority: number;
    status: string;
    dueDate: DateTime;
    employeeId: number;
    employee: Employee
}