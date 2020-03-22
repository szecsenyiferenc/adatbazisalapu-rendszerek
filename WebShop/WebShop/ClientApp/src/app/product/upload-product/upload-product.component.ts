import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload-product',
  templateUrl: './upload-product.component.html',
  styleUrls: ['./upload-product.component.css']
})
export class UploadProductComponent implements OnInit {
  imageFile;
  imgURL: any;
  
  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
  }

  submit(f){
    let values: {productName: string, productPrice: number} = f.value;
    let product: Product = {
      id: 1,
      name: values.productName,
      price: values.productPrice,
      image: this.imgURL.substring(23)
    }
    this.productService.uploadProduct(product).subscribe(a => {
      if(a){
        this.router.navigateByUrl('/products');
      }
    });
  }

  onFileChanged(file) {
    let files = file.files

    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.imgURL = null;
      return;
    }

    this.readPreview(files);
    this.readFile(files);
  }

  private readPreview(files){
    var reader = new FileReader();
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgURL = reader.result; 
    }
  }

  private readFile(files){
    var reader = new FileReader();
    reader.readAsArrayBuffer(files[0]); 
    reader.onload = (_event) => { 
      var arrayBuffer = reader.result as ArrayBuffer;
      let array = new Uint8Array(arrayBuffer);
      this.imageFile = array;
    }
  }

}
