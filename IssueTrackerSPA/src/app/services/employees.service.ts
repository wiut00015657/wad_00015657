import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../types/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
apiurl: string = "http://localhost:5058/api/employees/"
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiurl);
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(this.apiurl  + id);
  }

  deleteEmployee(id: number) {
    return this.http.delete(this.apiurl  + id);
  }

  updateEmployee(id: number, data: Employee) {
    return this.http.put(this.apiurl  + id, data);
  }

  createEmployee(data: Employee) 
  {
    return this.http.post(this.apiurl, data);
  }
}
