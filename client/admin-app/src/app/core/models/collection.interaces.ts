export interface CollectionRequest {
    pageSize: number,
    pageNumber: number
}

export interface CollectionResponse<T> {
    total: number;
    items: Array<T>;
    pageSize: number;
    pageNumber: number;
}
