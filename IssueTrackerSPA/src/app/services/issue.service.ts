import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Issue } from '../types/issue';

@Injectable({
  providedIn: 'root'
})
export class IssuesService {
  apiurl: string = "http://localhost:5058/api/issues/";

  constructor(private http: HttpClient) { }

  getIssues(): Observable<Issue[]> {
    return this.http.get<Issue[]>(this.apiurl);
  }

  getIssue(id: number): Observable<Issue> {
    return this.http.get<Issue>(`${this.apiurl}${id}`);
  }

  deleteIssue(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiurl}${id}`);
  }

  updateIssue(id: number, data: Issue): Observable<Issue> {
    return this.http.put<Issue>(`${this.apiurl}${id}`, data);
  }

  createIssue(data: Issue): Observable<Issue> {
    return this.http.post<Issue>(this.apiurl, data);
  }
}
