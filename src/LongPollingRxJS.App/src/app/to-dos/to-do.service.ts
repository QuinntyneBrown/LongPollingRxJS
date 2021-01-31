import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { ToDo } from './to-do';
import { Observable, timer } from 'rxjs';
import { delay, expand, map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<ToDo[]> {
    return this._client.get<{ toDos: ToDo[] }>(`${this._baseUrl}api/to-dos`)
      .pipe(
        map(x => x.toDos)
      );
  }

  public getById(options: { toDoId: string }): Observable<ToDo> {
    return this._client.get<{ toDo: ToDo }>(`${this._baseUrl}api/to-dos/${options.toDoId}`)
      .pipe(
        map(x => x.toDo)
      );
  }

  public remove(options: { toDo: ToDo }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/to-dos/${options.toDo.toDoId}`);
  }

  public create(options: { toDo: ToDo }): Observable<{ toDoId: string }> {
    return this._client.post<{ toDoId: string }>(`${this._baseUrl}api/to-dos`, { toDo: options.toDo });
  }  

  public update(options: { toDo: ToDo }): Observable<{ toDoId: string }> {
    return this._client.put<{ toDoId: string }>(`${this._baseUrl}api/to-dos`, { toDo: options.toDo });
  }  

  poll(interval: number = 5000): Observable<ToDo[]> {
    return timer(0, interval)
    .pipe(
      switchMap(_ => this.get())
    );
 }
}
