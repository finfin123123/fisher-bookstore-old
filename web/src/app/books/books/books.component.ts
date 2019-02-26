import { Component, OnInit } from '@angular/core';
import { CommonModule } from "@angular/common";
import { Book } from '../book';
import { BookRowComponent } from './book-row/book-row.component';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  books: Book[] = [
  {
    id:1,
    title: 'Ready Player One',
    author: 'Ernest Cline'
  },
  {
    id: 2,
    title: 'Catch 22',
    author: 'Joseph Heller'
  }
];


  constructor() { }

  ngOnInit() {
  }

}
