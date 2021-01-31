import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-to-do-detail',
  templateUrl: './to-do-detail.component.html',
  styleUrls: ['./to-do-detail.component.scss']
})
export class ToDoDetailComponent implements OnInit {

  public form = new FormGroup({
    toDo: new FormControl()
  });

  constructor() { }

  ngOnInit(): void {
  }

}
