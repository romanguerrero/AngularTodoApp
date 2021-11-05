import { Component, OnInit } from '@angular/core';
import { Todo, TodoServiceService } from '../todo-service.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css'],
})
export class TodoListComponent implements OnInit {
  constructor(public todoService: TodoServiceService) { }

  public todos: Todo[] = [];

  ngOnInit(): void {
    this.todoService.fetchTodos$().subscribe((todos) => (this.todos = todos));
  }
}
