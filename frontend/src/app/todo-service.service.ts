import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

export interface Todo {
  id: number;
  title: string;
  done: boolean;
  createdDate: Date | string;
}

@Injectable({
  providedIn: 'root',
})
export class TodoServiceService {
  baseUrl = environment.url;
  status: any;
  errorMessage: any;

  constructor(public http: HttpClient) { }

  fetchTodos$(): Observable<Todo[]> {
    return this.http.get(this.baseUrl + '/api/todo').pipe(
      tap((r: any) => console.log('response', r.data)),
      map((r: any) => r.data)
    );
  }

  deleteTodo(id: number) {
    return this.http.delete(this.baseUrl + '/api/todo/' + id).subscribe({
      next: data => {
        this.status = 'Delete successful';
      },
      error: error => {
        this.errorMessage = error.message;
        console.error('There was a deletion error!', error);
      }
    });
  }

  markDone(id: number) {
    const body = { 'done': true };

    return this.http.patch(this.baseUrl + '/api/todo/' + id, body).subscribe({
      next: data => {
        this.status = 'Marked done successful';
      },
      error: error => {
        this.errorMessage = error.message;
        console.error('There was a marked done error!', error);
      }
    });
  }

  addTodo(title: string) {
    const body = { 'title': title };
    return this.http.post(this.baseUrl + '/api/todo/', body).subscribe({
      next: data => {
        this.status = 'Todo added successfully';
      },
      error: error => {
        this.errorMessage = error.message;
        console.error('There was a creation error!', error);
      }
    });
  }

  changeTodo(id: number, title: string) {
    const body = { 'title': title };
    return this.http.patch(this.baseUrl + '/api/todo/' + id, body).subscribe({
      next: data => {
        this.status = 'Marked done successful';
      },
      error: error => {
        this.errorMessage = error.message;
        console.error('There was a marked done error!', error);
      }
    });
  }

  fetchCompleted(): Observable<Todo[]> {
    return this.http.get(this.baseUrl + '/api/todo?includeDone=true').pipe(
      tap((r: any) => console.log('response', r.data)),
      map((r: any) => r.data)
    );
  }
}
