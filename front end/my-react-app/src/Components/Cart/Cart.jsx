import { useContext, useEffect, useMemo, useState } from "react";
import { CartContext } from "../../Contexts/CartProvider";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

const Cart = () => {

    const [totalPrice, setTotalPrice] =  useState(0);
    const {cartProducts, setCartProducts} = useContext(CartContext);

    let priceAdding = 0;

    let cartp = [];

    const cart = JSON.parse(JSON.stringify(cartProducts));
    
    cart?.forEach(product=> priceAdding+=product.itemPrice);

    const addCount = (idx)=>{
        cartp = JSON.parse(JSON.stringify(cartProducts));
        cartp[idx].quantity++;
        cartp[idx].itemPrice = cartp[idx].price * cartp[idx].quantity;
        setCartProducts(cartp);
        return toast.success('you added one more of the same item successfully !');
    }
    
    const removeCount = (idx)=>{
        cartp = JSON.parse(JSON.stringify(cartProducts));
        if(cartp[idx].quantity > 1 ){
            cartp[idx].quantity--;
            cartp[idx].itemPrice = cartp[idx].price * cartp[idx].quantity;
        }
        else {
            if(cartp.length === 1){
                cartp.splice(idx,1);
                localStorage.setItem('allProducts',JSON.stringify(cartp));
                setCartProducts(cartp);
                return toast.success('you removed one item from the cart');
            }
            cartp.splice(idx,1);
            setCartProducts(cartp);
            return toast.success('you removed one item from the cart');
        }
        setCartProducts(cartp);
        return toast.success('you removed one more of the same item successfully !');
    }

    const clearProduct = (idx)=>{
        cartp = JSON.parse(JSON.stringify(cartProducts));
        cartp.splice(idx,1);
        localStorage.setItem('allProducts',JSON.stringify(cartp));
        setCartProducts(cartp);
        return toast.success('you removed one item from the cart');
    }

    useEffect(()=>{

        setTotalPrice(priceAdding);

    }, [totalPrice , cartProducts, priceAdding]);


    
    return (
        
        <div className="container mx-auto pb-28">
            <h2 className="py-10 h2 text-[--primary-color] text-center">total price : {totalPrice}<span className="px-3 p">EGP</span></h2>
            {
                cart?.map( (product , idx) =>
                <div key={idx} className="grid text-center lg:grid-cols-3 pb-7 pt-10 border-b border-black gap-10 items-center">
                        <div className="">
                            <img src={product.urls[0]} alt={`product ${idx}`}  className="mx-auto w-full "/>
                        </div>
                        <div>
                            <h3 className="h4 text-[--alternate2-color]">{product.title}</h3>
                            <p className="p py-5">item price : {product.itemPrice}EGP</p>
                            <p className="p py-5">item color : {product.color}</p>
                            <button onClick={()=>clearProduct(idx)} className="border rounded-full px-4 py-2  p bg-white text-black  hover:bg-[#DC3545] border-[#DC3545] hover:text-white  transition-all duration-500 h-fit mt-4 mx-auto block text-center ">REMOVE</button>
                        </div>
                        <div className="flex justify-between items-center">
                            <button onClick={()=>addCount(idx)} className="bg-[#14AE5C] border border-black rounded text-white py-1 px-3 mx-3 w-fit h-fit">+</button>
                            <p className="p">{product.quantity}</p>
                            <button onClick={()=>removeCount(idx)} className="bg-[#EC221F] border border-black rounded text-white py-1 mx-3 px-4 w-fit h-fit">-</button>
                        </div>
                </div>
                )
            }
            {cartProducts.length > 0 ? <Link to="/purchase" className="border border-black rounded-full px-4 py-2  p bg-black text-white hover:bg-white hover:border-black hover:text-black transition-all duration-500 h-fit mt-4 mx-auto block text-center w-full">PURCHASE</Link> : null}
        </div>
    );
}

export default Cart;
