import { BrandListModel } from './../models/brandListModel';
import { ListResponseModel } from './../../../core/models/listResponseModel';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  apiUrl= 'http://localhost:5023/api/Brands/'
  constructor(private httpClient:HttpClient) { }

  getBrands(page: number, size: number):Observable<ListResponseModel<BrandListModel>>{
    let newPath=this.apiUrl+ 'getAll?Page'+ page + "&PageSize="+ size;
    return this.httpClient.get<ListResponseModel<BrandListModel>>(newPath);
  }
}
