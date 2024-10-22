import { Link } from "react-router-dom";
import './About.css'

import product1 from '../../images/filtered products/wallet-women-14-1.png'
import product2 from '../../images/filtered products/wallet-women-12-1.png'
import product3 from '../../images/filtered products/bag-3-1.png'
import product4 from '../../images/filtered products/bag-4-1.png'



const About = () => {
    return (
        <div className='py-10'>
            <div className="container mx-auto py-12">
                <div className="gap-0 grid grid-cols-1 xl:grid-cols-2 md:gap-10">
                    <div className="grid grid-rows-3 gap-10 items-center">
                        <h1 className="h1 text-center text-[--primary-color]">handcrafted leather bags and wallets for the modern adventurer</h1>
                        <p className="sectionDesc text-[--alternate2-color]">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse varius enim in eros elementum tristique. Duis cursus, mi quis viverra ornare, eros dolor interdum nulla, ut commodo diam libero vitae erat.</p>
                        <div className="flex flex-col justify-around h-full">
                        <span><Link to="/bags" className="border border-black rounded-full   px-10 py-2 me-2 p text-black hover:bg-black hover:border-white hover:text-[--teritary-color] transition-all duration-500">BAGS</Link> <i className="fa-solid fa-arrow-right-long fa-fade text-lg"></i></span>
                        <span><Link to="/wallets" className="border border-black rounded-full   px-7 py-2 me-2 p text-black hover:bg-black hover:border-white hover:text-[--teritary-color] transition-all duration-500">WALLETS</Link> <i className="fa-solid fa-arrow-right-long fa-fade text-lg"></i></span>
                        </div>
                    </div>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-10 ">
                        <div >
                        <i className="fa-solid fa-wallet text-[--primary-color] text-3xl"></i>
                        <h4 className="h4 text-[--primary-color] py-5">Medium length section heading goes here </h4>
                        <p className="p text-[--alternate2-color]">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse varius enim in eros elementum tristique. Duis cursus, mi quis viverra ornare, eros dolor interdum nulla, ut commodo diam libero vitae erat.</p>
                        </div>
                        <div >
                        <i className="fa-solid fa-bag-shopping text-[--primary-color] text-3xl"></i>
                        <h4 className="h4 text-[--primary-color] py-5">Medium length section heading goes here </h4>
                        <p className="p text-[--alternate2-color]">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse varius enim in eros elementum tristique. Duis cursus, mi quis viverra ornare, eros dolor interdum nulla, ut commodo diam libero vitae erat.</p>
                        </div>
                        <div >
                        <i className="fa-solid fa-coins text-[--primary-color] text-3xl"></i>
                        <h4 className="h4 text-[--primary-color] py-5">Medium length section heading goes here </h4>
                        <p className="p text-[--alternate2-color]">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse varius enim in eros elementum tristique. Duis cursus, mi quis viverra ornare, eros dolor interdum nulla, ut commodo diam libero vitae erat.</p>
                        </div>
                        <div >
                        <i className="fa-solid fa-star text-[--primary-color] text-3xl"></i>
                        <h4 className="h4 text-[--primary-color] py-5">Medium length section heading goes here </h4>
                        <p className="p text-[--alternate2-color]">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse varius enim in eros elementum tristique. Duis cursus, mi quis viverra ornare, eros dolor interdum nulla, ut commodo diam libero vitae erat.</p>
                        </div>
                    </div>
                </div>
            </div>
            <div className="second-section-about bg-fixed bg-cover py-12 flex items-center justify-center">
                <h2 className="h2 text-[--teritary-color]">Some header</h2>
            </div>
            <div className="container mx-auto py-12">
                <h2 className="h2 text-[--primary-color] text-center">Our Journey in Leather Industry</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 2xl:grid-cols-4 gap-24 py-20 text-center items-center">
                    <div className="layer-parent mx-auto">
                        <img src={product1} alt="" className="w-full  h-full transition-all duration-500"/>
                        <div className="layer">
                        <Link className="h2" to="/wallets">shop now</Link>
                        </div>
                    </div>
                    <div className="layer-parent mx-auto">
                        <img src={product2} alt="" className="w-full h-full transition-all duration-500"/>
                        <div className="layer">
                        <Link className="h2" to="/wallets">shop now</Link>
                        </div>
                    </div>
                    <div className="layer-parent mx-auto">
                        <img src={product3} alt="" className="w-full h-full transition-all duration-500"/>
                        <div className="layer">
                        <Link className="h2" to="/bags">shop now</Link>
                        </div>
                    </div>
                    <div className="layer-parent mx-auto">
                        <img src={product4} alt="" className="w-full h-full transition-all duration-500"/>
                        <div className="layer">
                            <Link className="h2" to="/bags">shop now</Link>
                        </div>
                    </div>
                </div>
                <p className="sectionDesc text-center text-[--alternate2-color] py-12">Discover the rich history and evolution of our brand.</p>
            </div>
        </div>
    );
}

export default About;
