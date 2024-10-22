import { Link, useLocation } from "react-router-dom";
import logo from '../../images/icons/Artboard_1.svg';
import logo2 from '../../images/icons/Artboard_3.svg';
import { useContext, useEffect, useRef, useState } from "react";
import './Nav.css'
import { CartContext } from "../../Contexts/CartProvider";

const Nav = () => {
    const [isClicked, setIsClicked] = useState(false);
    const path = useLocation().pathname;
    const nav = useRef(null);
    const {cartProducts} = useContext(CartContext);

    useEffect(()=>{
        if(path === '/bags' || path === '/wallets'){
            nav.current.classList.add('z-10','absolute','top-0','left-0','w-full', 'text-[--primary-color]');
        }
        else {
            nav.current.classList.remove('z-10','absolute','top-0','left-0','w-full', 'text-[--primary-color]');
        }
    }, [path])

    const btnLogic = (e) => {
        const optionMenu = e.currentTarget.querySelector('.fa-bars');
        const navLinks = document.querySelectorAll('.navLinks');
        

        if (!isClicked) {
            navLinks.forEach((link) => {
                if( link.classList.contains('drop') ){
                    link.classList.add('visible', 'col-span-2', 'text-center', 'z-0');
                    link.classList.remove('hidden');
                }
                else {
                    link.classList.add('row-start-1');
                }
            });
            optionMenu.classList.add('fa-rotate-90');
            setIsClicked(true);
        } else if ( document.body.offsetWidth > 767 || isClicked){
            navLinks.forEach(link => {
                if( link.classList.contains('drop') ){
                    link.classList.remove('visible', 'col-span-2', 'text-center', 'z-0');
                    link.classList.add('hidden');
                }
                else {
                    link.classList.remove('row-start-1');
                }
            })
            optionMenu.classList.remove('fa-rotate-90');
            setIsClicked(false);
        }
    }; 

    return (
        <div>
            <div   className="container mx-auto w-screen relative">
                <div className="py-6" ref={nav}>
                    <ul className="grid md:grid-cols-6 grid-cols-2 md:text-center items-center gap-4">
                        <li className="hidden md:block drop navLinks h4" >
                            <Link to="/" className="hover:text-[--primary-color] transition-all duration-1000" >home</Link>
                        </li>
                        <li className="hidden md:block drop navLinks h4">
                            <Link to="/about" className="hover:text-[--primary-color] transition-all duration-1000" >about us</Link>
                        </li>
                        <li className="ms-7 navLinks">
                            <img src={path === '/bags' || path === '/wallets'? logo2 : logo} alt="" className="w-52" />
                        </li> 
                        <li className="hidden md:block drop navLinks h4">
                            <Link to="/bags" className="hover:text-[--primary-color] transition-all duration-1000" >bags</Link>
                        </li>
                        <li className="hidden md:block drop navLinks h4">
                            <Link to="/wallets" className="hover:text-[--primary-color] transition-all duration-1000" >wallets</Link>
                        </li>
                        <li className="hidden md:block drop navLinks h4">
                            <Link to="/cart" className="hover:text-[--primary-color] transition-all duration-1000 relative" >
                            cart <i className="fa-solid fa-cart-shopping ms-2"></i>
                            <div className="cartLayer flex items-center justify-center px-2 py-2  w-fit h-full rounded-full bg-red-600 absolute top-0 left-0 translate-x-20 -translate-y-5 text-white z-30">{cartProducts.length}+</div>
                            </Link>
                        </li>
                        <li className="md:hidden visible text-right me-5 navLinks h4">
                            <button onClick={btnLogic} type="button">
                                <i className="fa-solid fa-bars text-2xl transition-all duration-500"></i>
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default Nav;
