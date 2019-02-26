import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from "@angular/common";
import { Book } from '../../book';
import { BooksComponent } from '../books.component';

@Component({
  selector: 'app-book-row',
  templateUrl: './book-row.component.html',
  styleUrls: ['./book-row.component.css']
})
export class BookRowComponent implements OnInit {

  @Input() book: any;

  ngOnInit() {
  }

}
