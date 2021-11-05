import { Component, OnInit } from '@angular/core';
import { TodoServiceService } from '../todo-service.service';

@Component({
  selector: 'app-create-todo',
  templateUrl: './create-todo.component.html',
  styleUrls: ['./create-todo.component.css']
})
export class CreateTodoComponent implements OnInit {
  todoText = '';

  constructor(public todoService: TodoServiceService) { }

  ngOnInit(): void {
  }

  addTodo() {
    if (this.todoText === '') {
      alert('Todo cannot be empty!');
    } else {
      this.todoService.addTodo(this.todoText);
      setTimeout(() => { window.location.reload(); }, 50);
    }
  }
}
