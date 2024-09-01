import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { CollectionRequest, CollectionResponse } from '../core/models/collection.interaces';
import { Organization } from '../core/models/organization.interfaces';
import { ApiRoutes } from '../core/constants/routes';
import { ErrorResponse } from '../core/models/errorResponse';
import { InviteOrganization } from '../core/models/invite-organization';
import { SimpleResponse } from '../core/models/simple-response';

@Injectable({
  providedIn: 'root'
})
export class OrganizationsService {

  constructor(private httpClient: HttpClient) { }

  getOrganizationList(request: CollectionRequest): Promise<CollectionResponse<Organization> | ErrorResponse> {
    return lastValueFrom(this.httpClient.post<CollectionResponse<Organization>>(ApiRoutes.Organizations.GetOrganizationList, request));
  }

  inviteUserAsync(email: string, inviteAsOrgOwner: boolean) : Promise<ErrorResponse | SimpleResponse<boolean>> {
    let body: InviteOrganization = {
      email: email,
      inviteOrgOnwer: inviteAsOrgOwner
    };

    return lastValueFrom(this.httpClient.post<ErrorResponse | SimpleResponse<boolean>>(ApiRoutes.Organizations.Invitations, body));
  }
}
