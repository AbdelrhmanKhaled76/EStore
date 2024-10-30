import Footer from '../Footer/Footer';
import Nav from '../Nav/Nav';

const Layout = ({ children }) => {
    return (
        <div>
            <Nav />

            {children}

            <Footer />
        </div>
    );
}

export default Layout;
