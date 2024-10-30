import { useContext, useState } from 'react';
import { toast } from 'react-toastify';
import axios from 'axios';
import { url } from '../../App';
import { useParams } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import Loading from '../Loading/Loading';
import Slider from 'react-slick/lib/slider';
import './Product.css'
import { CartContext } from '../../Contexts/CartProvider';



const Product = () => {

    const {cartProducts, appendProduct, setCartProducts} = useContext(CartContext);

    const [selectedColor, setSelectedColor] = useState(null);

    const { Id } = useParams();

    const getProduct = ()=> axios.get(`${url}/api/products/${Id}`, 
        {
          headers : {
            'ngrok-skip-browser-warning' : 'asdasdas'
          }
        }
      );

    const {data, isError, isLoading} = useQuery({
      queryKey : ['getProduct'],
      queryFn : getProduct
    });

    if(isLoading){
      return <Loading />
    }

    if(isError){
      toast.error('An Error Has OccuRed Please Try Again !');
    }

    const settings = {
        infinite: false,
        arrows : false,
        speed: 500,
        dots: true,
        slidesToShow: 1,
    };

    const {data : product} = data;

    console.log(product);
    
    const addToCart = () => {
      
      
      if (!selectedColor) {
          return toast.error('Choose a color please!');
      }
      
      const allProducts = JSON.parse(JSON.stringify(cartProducts));
      
  
      // Check if the product with the same ID and color exists
      const existingProductIndex = allProducts?.findIndex(
          Product => (Product.id === product.id) && (Product.color === selectedColor)
      );
  
      if (existingProductIndex !== -1) {
          // If the product exists, update the quantity and item price
          allProducts[existingProductIndex].quantity++;
          allProducts[existingProductIndex].itemPrice = allProducts[existingProductIndex].quantity * product.price;
          setCartProducts(allProducts);
      } else {
          // If the product does not exist, append it
        const productCopy = JSON.parse(JSON.stringify(product));
        productCopy.color = selectedColor;
        productCopy.quantity = 1;
        productCopy.itemPrice = productCopy.price;
        appendProduct(productCopy);
      }
  
      // Update the localStorage and CartContext
      toast.success('Product added to cart!');
  };
  
  

    return (
        <div className='container mx-auto pb-40 pt-20'>
            <div key={product.id} className='grid grid-cols-1 lg:grid-cols-2  gap-12 items-center'>
            <div>
                <Slider {...settings}>
                    {product?.urls.map((image, idx)=> <div key={idx} className='h-[400px]' >
                        <img  src={image} alt={`product ${product.id}`}  className="w-full h-full object-contain"/>
                    </div>)
                  }
                </Slider>
            </div>
            <div>
                <div className='grid grid-rows-5 gap-10 items-center text-center lg:text-left'>
                    <h2 className=' text-[--primary-color] h1'>{product.title}</h2>
                    <p className='p text-[----alternate2-color]'>{product.description}</p>
                    <span className='h4  text-[----alternate2-color] me-3'>product price : <span className='p text-[----alternate2-color] px-4'>{product.price} EGP</span></span> 
                    <div className='flex justify-between lg:justify-start items-center'>
                        <span className='h4  text-[----alternate2-color] me-3'>valid colors </span>
                        <div>
                        <label htmlFor="Black" className='p text-[--alternate2-color]'>
        Black
      </label>
      <input
        type="radio"
        name="color"
        value="Black"
        id="Black"
        className="mx-2"
        onChange={()=> setSelectedColor("Black")}
      />
                        </div> 
                        
      <div>
      <label htmlFor="Havan" className='p text-[--alternate2-color]'>
        Havan
      </label>
      <input
        type="radio"
        name="color"
        value="Havan"
        id="Havan"
        className="mx-2"
        onChange={()=> setSelectedColor("Havan")}
      />
      </div>
      
      <div>
      <label htmlFor="Red" className='p text-[--alternate2-color]'>
        Red
      </label>
      <input
        type="radio"
        name="color"
        value="Red"
        id="Red"
        className="mx-2"
    onChange={()=> setSelectedColor("Red")}
        />
      </div>
      
      
      <div>
      <label htmlFor="Blue" className='p text-[--alternate2-color]'>
        Blue
      </label>
      <input
        type="radio"
        name="color"
        value="Blue"
        id="Blue"
        className="mx-2"
        onChange={()=> setSelectedColor("Blue")}
      />
      </div>
      
      
      <div>
      <label htmlFor="Brown" className='p text-[--alternate2-color]'>
        Brown
      </label>
      <input
        type="radio"
        name="color"
        value="Brown"
        id="Brown"
        className="mx-2"
        onChange={()=> setSelectedColor("Brown")}
      />
      </div>
      
                        </div>
                    <button onClick={()=>addToCart()} className="mx-auto lg:mx-0 border border-black rounded-full px-4 py-2  p text-black hover:bg-black hover:border-white hover:text-[--teritary-color] transition-all duration-500 w-fit h-fit">ADD PRODUCT</button>
                </div>
            </div>
        </div>
        </div>
        
        
    );
}

export default Product;
