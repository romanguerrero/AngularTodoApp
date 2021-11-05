import { Component, Input, OnInit, } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Todo, TodoServiceService } from '../todo-service.service';

@Component({
  selector: "todo",
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css'],
})
export class TodoItemComponent implements OnInit {

  constructor(private dialog: MatDialog, private snackbar: MatSnackBar, public todoService: TodoServiceService) { }

  @Input() public todo: Todo;
  public editing = false;
  public _title = '';
  public todos: Todo[] = [];

  ngOnInit(): void { }

  public toggleEdit() {
    this.editing = !this.editing;
    this._title = this.todo.title;
  }

  public update(updates: { title?: string; }) {
    this.editing = false;

    if (updates.title === '') {
      alert('Todo can\'t be empty!');
      setTimeout(() => { window.location.reload(); }, 50);

    } else if (this.todo.title !== updates.title) {
      // * Change todo in database if there is a change
      this.todoService.changeTodo(this.todo.id, updates.title);
      setTimeout(() => { window.location.reload(); }, 50);
    }
  }

  public delete() {
    // * Delete todo
    this.todoService.deleteTodo(this.todo.id);

    // * Refresh list
    // * Delay to wait for database to update
    setTimeout(() => { window.location.reload(); }, 50);
  }

  public done() {
    // * Marks Todo as done in database
    this.todoService.markDone(this.todo.id);
    setTimeout(() => { window.location.reload(); }, 50);
  }
}
