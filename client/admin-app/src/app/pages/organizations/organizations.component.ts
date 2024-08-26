import { Component } from '@angular/core';
import { OrganizationsService } from '../../services/organizations.service';
import { Organization } from '../../core/models/organization.interfaces';
import { CollectionResponse } from '../../core/models/collection.interaces';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-organizations',
  standalone: true,
  imports: [NzTableModule, DatePipe],
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

  loadData(pageSize: number, pageNumber: number) {
    this.loading = true;
    this.orgService.getOrganizationList({ pageNumber: pageNumber, pageSize: pageSize })
      .subscribe({
        next: (response:any) => {
          this.loading = false;
          if (!response.errors) {
            var res = response as CollectionResponse<Organization>;
            this.total = res.total;
            this.pageNumber = res.pageNumber;
            this.pageSize = res.pageSize;
            this.organizations = res.items;
          }
        },
        error: (err) => {
          this.loading = false;
        }
      });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, sort, filter } = params;
    //this.loadData(pageIndex, pageSize);
  }
}
