--
-- PostgreSQL database dump
--

-- Dumped from database version 13.6
-- Dumped by pg_dump version 13.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: customer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customer (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    address character varying(50) NOT NULL,
    mobile bigint NOT NULL,
    email character varying(50) NOT NULL
);


ALTER TABLE public.customer OWNER TO postgres;

--
-- Name: customer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customer_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customer_id_seq OWNER TO postgres;

--
-- Name: customer_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customer_id_seq OWNED BY public.customer.id;


--
-- Name: order; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."order" (
    id integer NOT NULL,
    order_date timestamp with time zone DEFAULT now() NOT NULL,
    delivery_date timestamp with time zone NOT NULL,
    customer_id integer
);


ALTER TABLE public."order" OWNER TO postgres;

--
-- Name: order_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.order_id_seq OWNER TO postgres;

--
-- Name: order_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.order_id_seq OWNED BY public."order".id;


--
-- Name: order_product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_product (
    order_id integer NOT NULL,
    product_id integer NOT NULL
);


ALTER TABLE public.order_product OWNER TO postgres;

--
-- Name: product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.product (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    price numeric(10,0) NOT NULL,
    order_id integer
);


ALTER TABLE public.product OWNER TO postgres;

--
-- Name: product_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.product_id_seq OWNER TO postgres;

--
-- Name: product_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;


--
-- Name: tag; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tag (
    id integer NOT NULL,
    name character varying NOT NULL,
    product_id integer NOT NULL
);


ALTER TABLE public.tag OWNER TO postgres;

--
-- Name: tag_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tag_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tag_id_seq OWNER TO postgres;

--
-- Name: tag_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tag_id_seq OWNED BY public.tag.id;


--
-- Name: customer id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customer ALTER COLUMN id SET DEFAULT nextval('public.customer_id_seq'::regclass);


--
-- Name: order id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order" ALTER COLUMN id SET DEFAULT nextval('public.order_id_seq'::regclass);


--
-- Name: product id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);


--
-- Name: tag id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tag ALTER COLUMN id SET DEFAULT nextval('public.tag_id_seq'::regclass);


--
-- Data for Name: customer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.customer (id, name, address, mobile, email) FROM stdin;
1	sandhya	hyd	9876543212	sandhy@gmail.com
2	Sam	knr	9876543216	sam@.com
3	sandhya	hyd	9876543212	sandhy@gmail.com
\.


--
-- Data for Name: order; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."order" (id, order_date, delivery_date, customer_id) FROM stdin;
1	2022-03-16 15:27:08.07503+05:30	2022-04-16 15:33:22.888+05:30	1
\.


--
-- Data for Name: order_product; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.order_product (order_id, product_id) FROM stdin;
1	1
\.


--
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.product (id, name, price, order_id) FROM stdin;
1	minni	10000	1
\.


--
-- Data for Name: tag; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tag (id, name, product_id) FROM stdin;
1	women	1
\.


--
-- Name: customer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customer_id_seq', 3, true);


--
-- Name: order_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_id_seq', 1, true);


--
-- Name: product_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.product_id_seq', 1, true);


--
-- Name: tag_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tag_id_seq', 1, true);


--
-- Name: customer customer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customer
    ADD CONSTRAINT customer_pkey PRIMARY KEY (id);


--
-- Name: order order_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order"
    ADD CONSTRAINT order_pkey PRIMARY KEY (id);


--
-- Name: product product_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pkey PRIMARY KEY (id);


--
-- Name: tag tag_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tag
    ADD CONSTRAINT tag_pkey PRIMARY KEY (id);


--
-- Name: order customer; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."order"
    ADD CONSTRAINT customer FOREIGN KEY (customer_id) REFERENCES public.customer(id) NOT VALID;


--
-- Name: order_product order; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_product
    ADD CONSTRAINT "order" FOREIGN KEY (order_id) REFERENCES public."order"(id) NOT VALID;


--
-- Name: order_product product; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_product
    ADD CONSTRAINT product FOREIGN KEY (product_id) REFERENCES public.product(id) NOT VALID;


--
-- Name: tag product; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tag
    ADD CONSTRAINT product FOREIGN KEY (product_id) REFERENCES public.product(id) NOT VALID;


--
-- PostgreSQL database dump complete
--

