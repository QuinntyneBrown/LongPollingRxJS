import { Component, OnInit } from '@angular/core';
import { ToDoService } from './to-dos/to-do.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    private readonly _toDoService: ToDoService
  ) {

  }

  ngOnInit() {
    this._toDoService.poll().subscribe();
  }
}
