import section2Pic from "../../images/home/section-2-sidepic-home.png";
import './Home.css'

const Home = () => {
    return (
        <div className="py-12">
            <div className="first-section-home h-screen bg-cover py-10 bg-no-repeat bg-center ">
                <div className="container mx-auto text-center flex items-center flex-col justify-center h-full">
                    <h1 className='h1  text-[var(--primary-color)] py-16 lg:py-32 '>handcrafted leather bags and wallets for the modern adventurer</h1>
                    <p className='sectionDesc py-3  text-[var(--teritary-color)]'> Discover the Perfect Blend of Style and Functionality with Our Exquisite Leather Creations</p>
                </div>
            </div>
            <div className='py-20 lg:py-52 container mx-auto'>
                <div className='grid grid-cols-1 xl:grid-cols-2 gap-x-10'>
                    <div className='text-center flex items-center flex-col justify-center'>
                        <h2 className='h2 text-[var(--primary-color)]'>Experience the Artistry and Durability of Our Handmade Leather Products</h2>
                        <h4 className='sectionDesc py-10 text-[var(--alternate2-color)]'>Each of our leather bags and wallets is meticulously crafted by skilled artisans using the finest materials. With attention to detail and a focus on quality, our products are built to last and age beautifully over time.</h4>
                    </div>
                    <div className='mx-auto hidden xl:block'>
                        <img src={section2Pic} alt="" className='w-100' />
                    </div>
                </div>
            </div>
            <div className="second-section-home  py-24  mb-16 bg-no-repeat bg-cover bg-top">
                <div className="container mx-auto flex flex-col items-center justify-center text-center">
                    <h2 className='h2 py-5 text-[var(--primary-color)]'>Experience the Artistry and Durability </h2>
                    <p className='sectionDesc pb-20 pt-10 text-[var(--teritary-color)]'>Each of our leather bags and wallets is meticulously crafted by skilled artisans using the</p>
                </div>
            </div>
        </div>
    );
}

export default Home;
