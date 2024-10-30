import { Link } from "react-router-dom";
import logo from '../../images/icons/Artboard_1.svg';

const Footer = () => {
    return (
        <div>
            <div className="container mx-auto py-5">
                    <ul className="grid grid-cols-2 md:grid-cols-4 pb-10 xl:grid-cols-5 text-center items-center gap-4 border-b border-solid border-black">
                        <li className="h4 " >
                            <Link to="/" className="hover:text-[--primary-color] transition-all duration-1000">home</Link>
                        </li>
                        <li className="h4 ">
                            <Link to="/about" className="hover:text-[--primary-color] transition-all duration-1000">about us</Link>
                        </li>
                        <li className="ms-7 -translate-y-20 hidden xl:block">
                            <img src={logo} alt="" className="w-52" />
                        </li> 
                        <li className=" h4 ">
                            <Link to="/bags" className="hover:text-[--primary-color] transition-all duration-1000">bags</Link>
                        </li>
                        <li className=" h4 ">
                            <Link to="/wallets" className="hover:text-[--primary-color] transition-all duration-1000">wallets</Link>
                        </li>
                    </ul>
                    <div className="py-5 grid grid-cols-2 md:grid-cols-3 gap-5 items-center">
                        <p>© 2024 Handmade Leather Works. All rights reserved.</p>
                        <div className="h2 underline text-right md:text-center"><Link to="/policy" className="hover:text-[--primary-color] transition-all duration-1000" >Policy</Link></div>
                        <ul className="grid  col-span-2 md:col-span-1 grid-cols-3 items-center text-center py-7">
                            <li><a href="https://www.facebook.com/AlaaKhaledDesign" target="_blank" className="text-2xl border border-black px-3 py-1 rounded-full transition-all duration-1000 hover:bg-[#0866ff] hover:text-white hover:border-[#0866ff]"><i className="fa-brands fa-facebook-f"></i></a></li>
                            <li><a href="https://www.instagram.com/alaakhaleddesign/" target="_blank" className="text-2xl border border-black px-2 py-1 rounded-full transition-all duration-1000 hover:bg-gradient-to-r hover:from-[#833ab4] hover:via-[#fd1d1d] hover:to-[#fcb045] hover:text-white hover:border-[#833ab4]"><i className="fa-brands fa-instagram"></i></a></li>
                            <li><a href="https://www.tiktok.com/@alaa.khaled.design" target="_blank" className="text-2xl border border-black px-2 py-1 rounded-full transition-all duration-1000 hover:bg-black hover:text-white hover:border-black"><i className="fa-brands fa-tiktok"></i></a></li>
                        </ul>
                    </div>
            </div>
        </div>
    );
}

export default Footer;
