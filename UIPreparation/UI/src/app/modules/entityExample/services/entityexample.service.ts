import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EntityExample } from '../models/EntityExample';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EntityExampleService {

  constructor(private httpClient: HttpClient) { }


  getEntityExampleList(): Observable<EntityExample[]> {

    return this.httpClient.get<EntityExample[]>(environment.getApiUrl + '/entityExamples/getall')
  }

  getEntityExampleById(id: number): Observable<EntityExample> {
    return this.httpClient.get<EntityExample>(environment.getApiUrl + '/entityExamples/getbyid?id='+id)
  }

  addEntityExample(entityExample: EntityExample): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/entityExamples/', entityExample, { responseType: 'text' });
  }

  updateEntityExample(entityExample: EntityExample): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/entityExamples/', entityExample, { responseType: 'text' });

  }

  deleteEntityExample(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/entityExamples/', { body: { Id: id } });
  }


}