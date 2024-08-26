import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CollectionRequest, CollectionResponse } from '../core/models/collection.interaces';
import { Organization } from '../core/models/organization.interfaces';
import { ApiRoutes } from '../core/constants/routes';
import { ErrorResponse } from '../core/models/errorResponse';

@Injectable({
  providedIn: 'root'
})
export class OrganizationsService {

  constructor(private httpClient: HttpClient) { }

  getOrganizationList(request: CollectionRequest): Observable<CollectionResponse<Organization> | ErrorResponse> {
    return this.httpClient.post<CollectionResponse<Organization>>(ApiRoutes.Organizations.GetOrganizationList, request);
  }
}
