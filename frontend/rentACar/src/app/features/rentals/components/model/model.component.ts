import { ModelListModel } from './../../models/modelListModel';
import { Component, OnInit } from '@angular/core';
import { ListResponseModel } from 'src/app/core/models/listResponseModel';
import { ModelService } from '../../services/model.service';
import { PrimeNGConfig, SelectItem } from 'primeng/api';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.scss']
})
export class ModelComponent implements OnInit {
  sortOptions: SelectItem[];

  sortOrder: number;

  sortField: string;
  constructor(private modelService:ModelService, private primengConfig: PrimeNGConfig) { }
  models: ModelListModel[]=[]
  selectedModel : ModelListModel;
  ngOnInit(): void {
    this.getModel();
  }
  getModel(){
    this.modelService.getModel(0,100).subscribe(data=>{
      this.models=data.items;
    });
    this.sortOptions = [
      {label: 'Price High to Low', value: '!dailyPrice'},
      {label: 'Price Low to High', value: 'dailyPrice'}
  ];

  this.primengConfig.ripple = true;
  }
  onSortChange(event: any) {
    let value = event.value;

    if (value.indexOf('!') === 0) {
        this.sortOrder = -1;
        this.sortField = value.substring(1, value.length);
    }
    else {
        this.sortOrder = 1;
        this.sortField = value;
    }
  }
}
