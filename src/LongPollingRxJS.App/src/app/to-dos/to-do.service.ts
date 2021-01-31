import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { ToDo } from './to-do';
import { BehaviorSubject, Observable, timer } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

  private readonly _toDos$: BehaviorSubject<ToDo[]> = new BehaviorSubject([] as any);

  private _isPolling: boolean = false;
 
  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) {  }

  public get(): Observable<ToDo[]> {
    return this._client.get<{ toDos: ToDo[] }>(`${this._baseUrl}api/to-dos`)
      .pipe(
        map(x => x.toDos)
      );
  }

  public getSince(lastModified?:string): Observable<ToDo[]> {
    return this._client.get<{ toDos: ToDo[] }>(`${this._baseUrl}api/to-dos/since/${lastModified}`)
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

    this._toDos$.next(this.pluckOut({ items: this._toDos$.value, value: options.toDo.toDoId, key: "toDoId"}));

    return this._client.delete<void>(`${this._baseUrl}api/to-dos/${options.toDo.toDoId}`);
  }

  public create(options: { toDo: ToDo }): Observable<{ toDo: ToDo }> {
    return this._client.post<{ toDo: ToDo }>(`${this._baseUrl}api/to-dos`, { toDo: options.toDo });
  }  

  public update(options: { toDo: ToDo }): Observable<{ toDo: ToDo }> {
    return this._client.put<{ toDo: ToDo }>(`${this._baseUrl}api/to-dos`, { toDo: options.toDo })
    .pipe(
      tap(x => {
        var index = this._toDos$.value.map(x => x.toDoId).indexOf(x.toDo.toDoId);
        this._toDos$.value[index] = x.toDo;
        this._toDos$.next(this._toDos$.value);
      })
    );
  }  

  public poll(interval: number = 300): Observable<ToDo[]> {           
    if(!this._isPolling) {
      this._isPolling = true;
      this._lastModified = `January 1, 1900`;

      timer(0, interval)
      .pipe(
        switchMap(_ => this.getSince(this._lastModified)),
        tap(x => {   
          this._toDos$.next(this._toDos$.value.concat(x));          
          let toDos = this._toDos$.value?.sort((x,y) => new Date(y.modified).getTime() - new Date(x.modified).getTime());
          this._lastModified = toDos[0]?.modified || `January 1, 1900`;
        })
      ).subscribe();
    }

    return this._toDos$.asObservable();
 }
 
 private _lastModified: string;
 
 private pluckOut = (options: { items:any[], value:any, key: string}) => {
   var index = options.items.map(x => x[options.key]).indexOf(options.value);
   options.items.splice(index, 1);
   return options.items;
  }
}