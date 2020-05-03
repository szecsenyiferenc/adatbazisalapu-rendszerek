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
  fileType: any;
  prodName;
  prodPrice;

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
    if(this.productService.selectedProduct){
      this.prodName = this.productService.selectedProduct.name;
      this.prodPrice = this.productService.selectedProduct.price;
  
      //@ts-ignore
      var arrayBuffer = this.productService.selectedProduct.image as ArrayBuffer;
      let array = new Uint8Array(arrayBuffer);
      this.imageFile = array;
    }
  }

  submit(f){
    let values: {productName: string, productPrice: number} = f.value;

    let image = null;

    if(this.imgURL){
      image = this.fileType === "image/jpeg" ? this.imgURL.substring(23) : this.imgURL.substring(22)
    }

    let product: Product = {
      id: 1,
      name: values.productName,
      price: values.productPrice,
      image: image
    }

    if(this.productService.selectedProduct){
      product.id = this.productService.selectedProduct.id;

      if(!image){
        product.image = this.productService.selectedProduct.image;
      }
      
      this.productService.updateProduct(product).subscribe(a => {
        if(a){
          this.router.navigateByUrl('/products');
        }
      });
    }
    else{
      this.productService.uploadProduct(product).subscribe(a => {
        if(a){
          this.router.navigateByUrl('/products');
        }
      });
    }
  
  
  }

  onFileChanged(file) {
    let files = file.files

    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.fileType = null;
      this.imgURL = null;
      return;
    }

    this.fileType = mimeType;

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
