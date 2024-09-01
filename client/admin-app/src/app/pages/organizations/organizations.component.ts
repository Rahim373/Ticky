import { Component } from '@angular/core';
import { OrganizationsService } from '../../services/organizations.service';
import { Organization } from '../../core/models/organization.interfaces';
import { CollectionResponse } from '../../core/models/collection.interaces';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { DatePipe } from '@angular/common';
import { ContainerHeaderComponent } from '../../components/layout/container-header';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { InvitationModalComponent } from '../../components/shared/invitation-modal/invitation-modal.component';

@Component({
  selector: 'app-organizations',
  standalone: true,
  imports: [NzTableModule, DatePipe, ContainerHeaderComponent, NzButtonComponent, InvitationModalComponent ],
  templateUrl: './organizations.component.html'
})
export class OrganizationsComponent {
  total: number = 1;
  pageSize: number = 15;
  pageNumber: number = 1;
  organizations: Organization[] = [];
  loading: boolean = true;

  constructor(private orgService: OrganizationsService) { }

  ngOnInit() {
    this.loadData(this.pageSize, this.pageNumber);
  }

  async loadData(pageSize: number, pageNumber: number) {
    this.loading = true;
    let  response = await this.orgService.getOrganizationList({ pageNumber: pageNumber, pageSize: pageSize });

    if (!("errors" in response)) {
      var res = response as CollectionResponse<Organization>;
      this.total = res.total;
      this.pageNumber = res.pageNumber;
      this.pageSize = res.pageSize;
      this.organizations = res.items;
    }
    
    this.loading = false;
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, sort, filter } = params;
    //this.loadData(pageIndex, pageSize);
  }
}
