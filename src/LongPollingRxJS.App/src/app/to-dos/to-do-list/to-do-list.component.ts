import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '@shared/dialog.service';
import { combineLatest, Observable, of, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ToDo } from '../to-do';
import { ToDoDetailComponent } from '../to-do-detail/to-do-detail.component';
import { ToDoService } from '../to-do.service';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss']
})
export class ToDoListComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();

  public readonly vm$: Observable<{
    dataSource: MatTableDataSource<ToDo>,
    columnsToDisplay: string[]
  }> = combineLatest([
    this._toDoService.get(),
    of(["name","actions"])
  ])
  .pipe(
    map(([toDos, columnsToDisplay]) => {
      return {
        dataSource: new MatTableDataSource(toDos),
        columnsToDisplay
      }
    })
  )

  constructor(
    private readonly _toDoService: ToDoService,
    private readonly _dialogService: DialogService
  ) { }

  public edit() {
    this._dialogService.open<ToDoDetailComponent>(ToDoDetailComponent);
  }

  public create() {
    this._dialogService.open<ToDoDetailComponent>(ToDoDetailComponent);
  }

  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }

}
