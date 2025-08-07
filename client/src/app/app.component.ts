import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/products';
import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class App implements OnInit {
  baseURl = 'http://localhost:5135/api/'
  private http = inject(HttpClient);
  title = 'Volleyball eShop';
  products: Product[] = [];

  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseURl + 'products').subscribe({
      next: response => this.products = response.data,
      error:error => console.log (error),
      complete: () => console.log('Product fetch operation completed.')
    });
  }
  
}
