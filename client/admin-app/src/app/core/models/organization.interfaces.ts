export interface Organization {
    name: number;
    createdOn: Date;
    updatedOn: Date;
    isActive: boolean;
    owner: OrganizationOwner;
}

export interface OrganizationOwner {
    id: string;
    email: string;
}