import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import axiosInstance from '../../services/axios-config';
@Component({
  selector: 'app-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.css']
})
export class ContentComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  data: any;
  id: number = 0;

  // params => {
  //   const id = params['id'];
  //   this.id = id;
  // }
  
  async ngOnInit() {
    this.route.params.subscribe();
    try {
      const response = await axiosInstance.get(`/category/all`);
      this.data = response.data;
    } catch (error) {
      console.error('Erro ao obter dados da API:', error);
    }
  }
}
