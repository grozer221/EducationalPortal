import React, {useCallback, useEffect, useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {Navigate, useNavigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {AutoComplete, Form, Input, message, Tag} from 'antd';
import {ButtonUpdate} from '../../../../../../components/ButtonUpdate/ButtonUpdate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {GET_SUBJECT_QUERY, GetSubjectData, GetSubjectVars} from '../../subjects.queries';
import {UPDATE_SUBJECT_MUTATION, UpdateSubjectData, UpdateSubjectVars} from '../../subjects.mutations';
import Title from 'antd/es/typography/Title';
import {Grade} from '../../../grades/grades.types';
import Search from 'antd/es/input/Search';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../../grades/grades.queries';
import debounce from 'lodash.debounce';

type FormValues = {
    id: string,
    name: string,
    link: string,
}

export const SubjectsUpdate = () => {
    const params = useParams();
    const id = params.id as string;
    const [updateSubjectMutation, updateSubjectMutationOption] = useMutation<UpdateSubjectData, UpdateSubjectVars>(UPDATE_SUBJECT_MUTATION);
    const [form] = Form.useForm();
    const getSubjectQuery = useQuery<GetSubjectData, GetSubjectVars>(GET_SUBJECT_QUERY,
        {variables: {id: id}},
    );
    const navigate = useNavigate();
    const [gradePage, setGradePage] = useState(1);
    const [gradeLike, setGradeLike] = useState('');
    const getGradesQuery = useQuery<GetGradesData, GetGradesVars>(GET_GRADES_QUERY, {
        variables: {
            page: gradePage,
            like: gradeLike,
        },
    });
    const [grades, setGrades] = useState<Grade[]>([]);

    useEffect(() => {
        getGradesQuery.refetch({
            page: gradePage,
            like: gradeLike,
        })
            .then(response => {
                if (!response.data.getGrades.entities.length) {
                    message.warning('Класів з даною назвою не знайдено');
                }
            })
            .catch(error => {
                message.error(error.message);
            });
    }, [gradePage, gradeLike]);

    useEffect(() => {
        getSubjectQuery.data && setGrades(getSubjectQuery.data.getSubject.gradesHaveAccessRead);
    }, [getSubjectQuery.data?.getSubject]);

    const onFinish = async ({id, name, link}: FormValues) => {
        updateSubjectMutation({
            variables: {
                updateSubjectInputType: {
                    id,
                    name,
                    link,
                    gradesHaveAccessReadIds: grades.map(grade => grade.id),
                },
            },
        })
            .then(response => {
                navigate('../');
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const debouncedSearchGradesHandler = useCallback(debounce(nextValue => setGradeLike(nextValue), 500), []);
    const searchGradesHandler = (value: string) => debouncedSearchGradesHandler(value);

    const selectGradeHandler = (value: string) => {
        const newGrade = getGradesQuery.data?.getGrades.entities.find(grade => grade.name === value) as Grade;
        setGrades([...grades, newGrade]);
    };

    const removeGradeHandler = (value: string) => {
        const newGrades = grades.filter(grade => grade.name !== value);
        setGrades(newGrades);
    };

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getSubjectQuery.loading)
        return <Loading/>;


    const subject = getSubjectQuery.data?.getSubject;
    return (
        <Form
            name="EducationalYearsUpdateForm"
            onFinish={onFinish}
            form={form}
            initialValues={{
                id: subject?.id,
                name: subject?.name,
                link: subject?.link,
            }}
            {...sizeFormItem}
        >
            <Title level={2}>Редагування предмету</Title>
            <Form.Item name="id" style={{display: 'none'}}>
                <Input type={'hidden'}/>
            </Form.Item>
            <Form.Item
                name="name"
                label="Назва"
                rules={[{required: true, message: 'Введіть назву!'}]}
            >
                <Input placeholder="Назва"/>
            </Form.Item>
            <Form.Item
                name="link"
                label="Посилання"
            >
                <Input placeholder="Посилання"/>
            </Form.Item>
            <Form.Item
                label="Класи"
            >
                <AutoComplete
                    options={getGradesQuery.data?.getGrades.entities.map(grade => ({value: grade.name}))}
                    onSearch={searchGradesHandler}
                    onSelect={selectGradeHandler}
                >
                    <Search
                        placeholder="Класи"
                        enterButton
                        loading={getGradesQuery.loading}
                    />
                </AutoComplete>
                {grades.map(grade => (
                    <Tag
                        closable
                        onClose={e => {
                            e.preventDefault();
                            removeGradeHandler(grade.name);
                        }}
                    >{grade.name}</Tag>
                ))}
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonUpdate loading={updateSubjectMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
