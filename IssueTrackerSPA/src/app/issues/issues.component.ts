import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Issue } from '../types/issue';
import { IssuesService } from '../services/issue.service';
import { ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-issues',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './issues.component.html',
  styleUrls: ['./issues.component.css']
})
export class IssuesComponent implements OnInit {
  issues$!: Observable<Issue[]>;
  issueService = inject(IssuesService);

  constructor(private toastrService: ToastrService) {}

  ngOnInit(): void {
    this.loadIssues();
  }

  deleteIssue(id: number): void {
    this.issueService.deleteIssue(id).subscribe({
      next: () => {
        this.toastrService.success('Issue successfully deleted');
        this.loadIssues(); 
      },
      error: (err: any) => {
        console.error(err);
        this.toastrService.error('Failed to delete issue');
      }
    });
  }

  private loadIssues(): void {
    this.issues$ = this.issueService.getIssues();
  }
}
