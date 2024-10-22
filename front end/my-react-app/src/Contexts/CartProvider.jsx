import { createContext, useState, useEffect } from "react";

export const CartContext = createContext();

const CartProvider = ({ children }) => {

    const [cartProducts, setCartProducts] = useState([]);

    useEffect(() => {
        const storedProducts = JSON.parse(localStorage.getItem('allProducts'));
        if (storedProducts) {
            setCartProducts(storedProducts); 
        }
    }, []);

    useEffect(()=>{

        const storedProducts = JSON.parse(JSON.stringify(cartProducts));

        if (storedProducts.length > 0) {
            localStorage.setItem('allProducts', JSON.stringify(cartProducts));
        }

    }, [cartProducts]);

    const appendProduct = (obj) => {

        const updatedCartProducts = JSON.parse(JSON.stringify(cartProducts));

        updatedCartProducts.push(obj);

        setCartProducts(updatedCartProducts);

    };

    return (
        <CartContext.Provider value={{ cartProducts, appendProduct, setCartProducts }}>
            {children}
        </CartContext.Provider>
    );
}

export default CartProvider;
